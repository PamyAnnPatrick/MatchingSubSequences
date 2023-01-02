using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchingSubSequences
{

    public class TrieSolution
    {
        Trie root = new Trie();

        public int NumMatchingSubseq(string s, string[] words)
        {
            int count = 0;
            CreateTrie(words);

            foreach (char c in s)
            {
                if (root.Children.TryGetValue(c, out Trie matchChild))
                {
                    count += matchChild.LeafCount;

                    root.Children.Remove(c);

                    if (matchChild.Children.Count > 0)
                    {
                        MergeTrie(matchChild, root);
                    }

                }
            }

            return count;
        }

        private void MergeTrie(Trie matchChild, Trie parent)
        {

            foreach (var child in matchChild.Children)
            {
                if (parent.Children.ContainsKey(child.Key))
                {
                    if (child.Value.LeafCount > 0)
                        parent.Children[child.Key].LeafCount += child.Value.LeafCount;

                    if (child.Value.Children.Count > 0)
                    {
                        MergeTrie(child.Value, parent.Children[child.Key]);
                    }
                }
                else
                {
                    parent.Children.Add(child.Key, child.Value);
                }

            }
        }

        private void CreateTrie(string[] words)
        {
            Trie current = root;
            foreach (var word in words)
            {
                current = root;
                foreach (char c in word)
                {
                    if (!current.Children.ContainsKey(c))
                        current.Children.Add(c, new Trie());

                    current = current.Children[c];
                }
                current.LeafCount++;
            }
        }
    }
    public class Trie
    {
        public int LeafCount;
        public Dictionary<char, Trie> Children;

        public Trie()
        {
            Children = new Dictionary<char, Trie>();
            LeafCount = 0;
        }
    }
}
