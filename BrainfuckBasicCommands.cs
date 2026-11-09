using System;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		private const string ConstantSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			RegisterInputOutputCommands(vm, read, write);
			RegisterMemoryCommands(vm);
			RegisterConstantCommands(vm);
		}

		private static void RegisterInputOutputCommands(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', machine =>
			{
				write((char)machine.Memory[machine.MemoryPointer]); // Вывести байт памяти, на который указывает указатель, преобразовав в символ согласно ASCII
			});

			vm.RegisterCommand(',', machine =>
			{
				var character = read();
				// Отрицательное значение обозначает конец ввода: текущая ячейка не изменяется.
				if (character >= 0)
				{
					machine.Memory[machine.MemoryPointer] = (byte)character; // Ввести символ и сохранить его ASCII-код в байт памяти, на который указывает указатель
				}
			});
		}

		private static void RegisterMemoryCommands(IVirtualMachine vm)
		{
			// Маска оставляет младшие 8 бит даже при включённой проверке переполнения.
			vm.RegisterCommand('+', machine =>
			{
				machine.Memory[machine.MemoryPointer] = (byte)((machine.Memory[machine.MemoryPointer] + 1) & 0xFF); // Увеличить байт памяти, на который указывает указатель
			});

			vm.RegisterCommand('-', machine =>
			{
				machine.Memory[machine.MemoryPointer] = (byte)((machine.Memory[machine.MemoryPointer] - 1) & 0xFF); // Уменьшить байт памяти, на который указывает указатель
			});

			vm.RegisterCommand('>', machine =>
			{
				machine.MemoryPointer = (machine.MemoryPointer + 1) % machine.Memory.Length; // Сдвинуть указатель памяти вправо на 1 байт
			});

			vm.RegisterCommand('<', machine =>
			{
				machine.MemoryPointer = (machine.MemoryPointer - 1 + machine.Memory.Length) % machine.Memory.Length; // Сдвинуть указатель памяти влево на 1 байт
			});
		}

		private static void RegisterConstantCommands(IVirtualMachine vm)
		{
			foreach (var symbol in ConstantSymbols)
			{
				vm.RegisterCommand(symbol, machine =>
				{
					machine.Memory[machine.MemoryPointer] = (byte)symbol; // Сохраняет символ в байт памяти на указанный указатель
				});
			}
		}
	}
}