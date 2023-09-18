namespace TreeCtrl
{
    // public class TreeNode
    // {
    //     public string Name { get; set; } = "";
    //     public List<TreeNode> Children { get; set; } = new List<TreeNode>();
    // }
    public class TreeNode
    {
        public string Name { get; set; } = "";
        public List<TreeNode> Children { get; set; } = new List<TreeNode>();
        public TreeNode? Parent { get; set; } // 追加
    }

    public static class TreeGenerator
    {
        public static TreeNode GenerateFixedTree()
        {
            // ルートノード
            var root = new TreeNode { Name = "[Root]" };

            // 第1階層
            for (int i = 1; i <= 3; i++)
            {
                var firstLevelChild = new TreeNode { Name = $"Child_{i}", Parent = root };
                root.Children.Add(firstLevelChild);

                // 第2階層
                for (int j = 1; j <= 2; j++)
                {
                    var secondLevelChild = new TreeNode { Name = $"Child_{i}_{j}", Parent = firstLevelChild };
                    firstLevelChild.Children.Add(secondLevelChild);

                    // 第3階層
                    for (int k = 1; k <= 3; k++)
                    {
                        var thirdLevelChild = new TreeNode { Name = $"Child_{i}_{j}_{k}", Parent = secondLevelChild };
                        secondLevelChild.Children.Add(thirdLevelChild);

                        // 第4階層
                        for (int l = 1; l <= 4; l++)
                        {
                            var fourthLevelChild = new TreeNode { Name = $"Child_{i}_{j}_{k}_{l}", Parent = thirdLevelChild };
                            thirdLevelChild.Children.Add(fourthLevelChild);
                        }
                    }
                }
            }

            return root;
        }
    }

    public struct TreeOutputSettings
    {
        public string BranchIndicator;        // 分岐を示す文字
        public string EndBranchIndicator;     // 最後の分岐を示す文字
        public string Indentation;            // インデント文字
        public string SimpleIndentation;      // シンプルなインデント（スペースやタブなど）

        public static TreeOutputSettings PlusMinusStyle => new TreeOutputSettings
        {
            BranchIndicator = "+-- ",
            EndBranchIndicator = "+-- ",
            Indentation = "|   ",
            SimpleIndentation = "    "
        };

        public static TreeOutputSettings UnicodeBoxStyle => new TreeOutputSettings
        {
            BranchIndicator = "├── ",
            EndBranchIndicator = "└── ",
            Indentation = "│   ",
            SimpleIndentation = "    "
        };

        public static TreeOutputSettings SimpleStyle => new TreeOutputSettings
        {
            BranchIndicator = "",
            EndBranchIndicator = "",
            Indentation = "",
            SimpleIndentation = "    "
        };
    }

    public static class TreePrinter
    {
        public static void Print(TreeNode node, TreeOutputSettings settings)
        {
            PrintRecursive(node, "", true, settings);
        }

        public static void PrintPathToRoot(TreeNode node, TreeOutputSettings settings)
        {
            var pathToRoot = new Stack<TreeNode>();
            while (node != null)
            {
                pathToRoot.Push(node);
                node = node.Parent;
            }

            PrintRecursive(pathToRoot, "", true, settings);
        }


        private static void PrintRecursive(TreeNode node, string indent, bool isLast, TreeOutputSettings settings)
        {
            var currentIndent = isLast ? settings.EndBranchIndicator : settings.BranchIndicator;
            Console.WriteLine(indent + currentIndent + node.Name);

            for (int i = 0; i < node.Children.Count; i++)
            {
                var isChildLast = (i == node.Children.Count - 1);
                var newIndent = isLast ? indent + settings.SimpleIndentation : indent + settings.Indentation;
                PrintRecursive(node.Children[i], newIndent, isChildLast, settings);
            }
        }

        private static void PrintRecursive(Stack<TreeNode> nodes, string indent, bool isLast, TreeOutputSettings settings)
        {
            if (nodes.Count == 0) return;

            var node = nodes.Pop();
            var currentIndent = isLast ? settings.EndBranchIndicator : settings.BranchIndicator;
            Console.WriteLine(indent + currentIndent + node.Name);

            for (int i = 0; i < node.Children.Count && nodes.Count > 0; i++)
            {
                var child = node.Children[i];
                if (child == nodes.Peek())
                {
                    var newIndent = isLast ? indent + settings.SimpleIndentation : indent + settings.Indentation;
                    PrintRecursive(nodes, newIndent, nodes.Count == 1, settings);
                }
            }
        }
    }
}