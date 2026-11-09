using System;

namespace func.brainfuck
{
	public interface IVirtualMachine
	{
		string Instructions { get; }

		/// <summary>
		/// Позиция текущей инструкции; после вызова обработчика Run увеличивает её на единицу.
		/// </summary>
		int InstructionPointer { get; set; }

		byte[] Memory { get; }
		int MemoryPointer { get; set; }

		/// <summary>
		/// Регистрирует обработчик символа, заменяя предыдущий при повторной регистрации.
		/// </summary>
		void RegisterCommand(char symbol, Action<IVirtualMachine> execute);

		/// <summary>
		/// Выполняет инструкции с текущей позиции, пропуская незарегистрированные символы.
		/// </summary>
		void Run();
	}
}