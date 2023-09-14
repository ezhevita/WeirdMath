using System.Runtime.InteropServices;

namespace WeirdMath;

public partial class Native
{
	[LibraryImport("libc", SetLastError = true)]
	public static partial int mprotect(IntPtr address, ulong length, MmapProts prot);

	[Flags]
	public enum MmapProts : uint
	{
		PROT_NONE = 0,
		PROT_READ = 1,
		PROT_WRITE = 2,
		PROT_EXEC = 4,
	}
}
