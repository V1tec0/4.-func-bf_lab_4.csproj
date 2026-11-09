using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		private readonly Dictionary<char, Action<IVirtualMachine>> commands;

		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program; // Исходный код программы на Brainfuck
			InstructionPointer = 0; // Нулевая точка инструкций 
			Memory = new byte[memorySize]; // Память с количеством ячеек в один байт
			MemoryPointer = 0; // Текущая памяти ячейки
			commands = new Dictionary<char, Action<IVirtualMachine>>(); // Словарь, где будет выпоняться код
		}

		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			commands[symbol] = execute; // Регистрируем символ с командой
		}

		public void Run()
		{
			while (InstructionPointer < Instructions.Length) // Цикл для исполненния всех команд
			{
				var command = Instructions[InstructionPointer]; // Символ команды

				if (commands.TryGetValue(command, out var execute)) // Проверка на символ, если он имеет команду, то исполняет
				{
					execute(this); // Вызываем команду определенного символа через делегат
				}

				// Команда может изменить указатель; прибавляем единицу уже к новому значению.
				InstructionPointer++; // После выполнения кода выполнения 
			}
		}
	}
}