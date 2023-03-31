using Kompil;
using System.IO;
using System.Runtime.CompilerServices;
try
{
    string code = "";
    string line;
    string name_file = Console.ReadLine();

    StreamReader s = new StreamReader(name_file);
    line = s.ReadLine();
    while (line != null)
    {
        code += line;
        line = s.ReadLine();
    }
    s.Close();


    Leksor Leksor = new Leksor(code);
    Leksor.lexAnalytic();

    Parser parser = new Parser(Leksor.tokens);

    var root = parser.parseCode();

    parser.run(root);

    
}
catch (Exception ex) { Console.WriteLine(ex.Message); }
Console.ReadKey();