#nullable disable
namespace WeirdMath;

public class WeirdMath
{
	public static void Main()
	{
		Prepare();
		Console.WriteLine("Please enter the first number:");
		var a = int.Parse(Console.ReadLine()!);
		Console.WriteLine("Please enter the second number:");
		var b = int.Parse(Console.ReadLine()!);
		Console.WriteLine($"{a} + {b} = {Add(a, b)}");
	}

	public static int Add(int a, int b)
	{
		return a + b;
	}

	private static unsafe void Prepare()
	{
		var ptr = (byte*) typeof(WeirdMath)
			.GetMethod(nameof(Add),
				System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)!
			.MethodHandle
			.GetFunctionPointer()
			.ToPointer();

		// doesn't work on macos for whatever reason
		var res = Native.mprotect((IntPtr) ((ulong) ptr & 0xFFFFFFFFFFFFF000),
			0x1000,
			Native.MmapProts.PROT_READ | Native.MmapProts.PROT_WRITE | Native.MmapProts.PROT_EXEC);

		if (res != 0)
			throw new Exception($"mprotect = {res}");

		// idk why i need so many nops, but without them it segfaults
		var replacement = new byte[] {
			0x90, // nop
			0x90, // nop
			0x90, // nop
			0x90, // nop
			0x90, // nop
			0x67, 0x8D, 0x44, 0x37, 0x01, // lea    eax,[edi+esi+1]
			0xC3 // ret
		};

		for (var i = 0; i < replacement.Length; i++)
		{
			ptr[i] = replacement[i];
		}
	}
}

