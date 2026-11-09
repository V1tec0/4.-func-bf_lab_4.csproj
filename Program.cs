using System;
using System.Linq;
using System.Text;
using NUnitLite;

namespace func.brainfuck
{
	public class Program
	{
		private const string SierpinskiTriangleBrainfuckProgram = @"
                                >
                               + +
                              +   -
                             [ < + +
                            +       +
                           + +     + +
                          >   -   ]   >
                         + + + + + + + +
                        [               >
                       + +             + +
                      <   -           ]   >
                     > + + >         > > + >
                    >       >       +       <
                   < <     < <     < <     < <
                  <   [   -   [   -   >   +   <
                 ] > [ - < + > > > . < < ] > > >
                [                               [
               - >                             + +
              +   +                           +   +
             + + [ >                         + + + +
            <       -                       ]       >
           . <     < [                     - >     + <
          ]   +   >   [                   -   >   +   +
         + + + + + + + +                 < < + > ] > . [
        -               ]               >               ]
       ] +             < <             < [             - [
      -   >           +   <           ]   +           >   [
     - < + >         > > - [         - > + <         ] + + >
    [       -       <       -       >       ]       <       <
   < ]     < <     < <     ] +     + +     + +     + +     + +
  +   .   +   +   +   .   [   -   ]   <   ]   +   +   +   +   +
";

		public static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			Console.InputEncoding = Encoding.UTF8;

			if (args.Contains("test"))
			{
				new AutoRun().Execute(Array.Empty<string>()); // Запуск тестов
			}
			else
			{
				RunDemo();
			}

			Console.ReadLine();
		}

		private static void RunDemo()
		{
			Brainfuck.Run(SierpinskiTriangleBrainfuckProgram, Console.Read, Console.Write);
			Console.WriteLine("Это была демонстрация Brainfuck на примере построения треугольника Серпинского");
		}
	}
}