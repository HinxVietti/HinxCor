using System;
using System.IO;

class Program{

static void Main(string[] args){
	
	Console.WriteLine("Welcome.!");
	Console.WriteLine();
	Console.WriteLine("Arguments");

	for(int i=0 ;i<args.Length;i++){
		Console.WriteLine("No:"+i+"\t"+args[i]);
	}
	Console.ReadKey();
    File.Create("~startok.mr");
	
}

}