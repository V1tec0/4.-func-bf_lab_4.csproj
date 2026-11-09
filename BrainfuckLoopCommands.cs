using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
			// Таблица принадлежит конкретной регистрации, а не разделяется между машинами.
			var loopBrackets = BuildLoopBrackets(vm.Instructions);

			vm.RegisterCommand('[', machine =>
			{
				// Если текущая ячейка равна 0, пропускаем цикл
				if (machine.Memory[machine.MemoryPointer] == 0)
				{
					// Переходим к соответствующей закрывающей скобке
					JumpToMatchingBracket(machine, loopBrackets);
				}
			});

			vm.RegisterCommand(']', machine =>
			{
				// Если текущая ячейка НЕ равна 0, повторяем цикл
				if (machine.Memory[machine.MemoryPointer] != 0)
				{
					// Переходим к соответствующей открывающей скобке
					JumpToMatchingBracket(machine, loopBrackets);
				}
			});
		}

		private static Dictionary<int, int> BuildLoopBrackets(string instructions)
		{
			// Заранее вычисляем все парные скобки 
			var loopBrackets = new Dictionary<int, int>(); // Место хранения скобок
			var openingBrackets = new Stack<int>(); // Стек для временного хранение скобок открытия цикла

			for (var index = 0; index < instructions.Length; index++)
			{
				if (instructions[index] == '[')
				{
					openingBrackets.Push(index); // Сохраняем в стек
				}
				else if (instructions[index] == ']')
				{
					var openingIndex = openingBrackets.Pop(); // Сохраняем во временную переменную и удаляем со стека
					loopBrackets[openingIndex] = index;
					loopBrackets[index] = openingIndex;
				}
			}

			return loopBrackets;
		}

		private static void JumpToMatchingBracket(IVirtualMachine vm, Dictionary<int, int> loopBrackets)
		{
			if (loopBrackets.TryGetValue(vm.InstructionPointer, out var matchingIndex))
			{
				// Следующий шаг Run увеличит указатель и начнёт выполнение после парной скобки.
				vm.InstructionPointer = matchingIndex;
			}
		}
	}
}