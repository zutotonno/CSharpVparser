# CSharpVparser
A C# csv parser for Math.net library

To have a Math.net Matrix<double> filled with a CSV simply add to your program those line of code below:

string path = "your_csv_file.csv"
/*second parameter in load matrix, indicates how many row parser has to skip*/

var retrieve = new LoadMatrix(path,0);

var rawData= retrieve.loadM();

retrieve.PartitionComplete += Generate_Complete;

retrieve.OnGenerateFinish();

private void Generate_Complete(object sender, EventArgs e)

{

Console.WriteLine("Matrix Ready");

}
