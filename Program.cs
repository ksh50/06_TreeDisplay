using TreeCtrl;

class Program
{
    static void Main()
    {
        var rootNode = TreeGenerator.GenerateFixedTree();
        // rootNode を利用してツリー構造を出力する処理等を行うことができます。

        // Console.WriteLine("Using + and - style:");
        // TreePrinter.Print(rootNode, TreeOutputSettings.PlusMinusStyle);

        // Console.WriteLine("\nUsing Unicode box drawing characters:");
        // TreePrinter.Print(rootNode, TreeOutputSettings.UnicodeBoxStyle);

        // Console.WriteLine("\nUsing simple indentation:");
        // TreePrinter.Print(rootNode, TreeOutputSettings.SimpleStyle);

        // 例としてChild_1_1_3_2ノードを取得
        var targetNode = rootNode.Children[0].Children[0].Children[2].Children[1];
        Console.WriteLine("Path from Child_1_1_3_2 to Root:");
        TreePrinter.PrintPathToRoot(targetNode, TreeOutputSettings.PlusMinusStyle);
    }
}
