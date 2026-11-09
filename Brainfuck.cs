using System;
using System.Text;

namespace func.brainfuck
{
	public class Brainfuck
	{
		public static string Run(string program, string input = "")
		{
			var inputIndex = 0;
			var output = new StringBuilder();
			// Делегат чтения возвращает -1 после исчерпания строки, как Console.Read.
			Run(program,
				() => inputIndex >= input.Length ? -1 : input[inputIndex++],
				character => output.Append(character));
			return output.ToString();
		}

		public static void Run(string program, Func<int> read, Action<char> write, int memorySize = 30000)
		{
			var machine = new VirtualMachine(program, memorySize);
			BrainfuckBasicCommands.RegisterTo(machine, read, write);
			BrainfuckLoopCommands.RegisterTo(machine);
			machine.Run();
		}
	}
}