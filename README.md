# WeirdMath

Some magic tricks in order to make .NET think that `2 + 2 = 5` (and generally any result is `actual = expected + 1`).

May work on Linux x86_64, everything else is not supported. Works both in Debug and Release modes.

Does not use any libraries, only .NET runtime methods (and a single `libc` import to unprotect the memory page for writing).
