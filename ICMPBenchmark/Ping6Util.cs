using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KinoDrawStill;

internal unsafe class Ping6Util
{
    static Ping6Util()
    {
        IntPtr packet = Marshal.AllocHGlobal(8);
        for (int i = 0; i < 8; i++)
            *(byte*)(packet + i) = 0;
        *(byte*)packet = 8;
        _payload = packet.ToPointer();
    }

    private static void* _payload;

    [DllImport("libc", EntryPoint = "socket", SetLastError = true)]
    public static extern int socket(int domain, int type, int protocol);
    [DllImport("libc", EntryPoint = "close", SetLastError = true)]
    public static extern int close(int handle);
    [DllImport("libc", EntryPoint = "connect", SetLastError = true)]
    private static extern int connect(int handle, ref sockaddr_in6 addr, int socklen_t);
    [DllImport("libc", EntryPoint = "send", SetLastError = true)]
    private static extern int send(int handle, void* message, int length, int flags);
    [DllImport("libc", EntryPoint = "shutdown", SetLastError = true)]
    private static extern int shutdown(int handle, int how);

    public static void KernelPing(int handle, byte[] address)
    {
        sockaddr_in6 sin = new sockaddr_in6()
        {
            sin_family = 10,
            sin6_port = 0,
            sin6_flowinfo = 0,
            sin6_addr = address,
            sin6_scope_id = 0
        };

        connect(handle, ref sin, 28);
        send(handle, _payload, 8, 0x40);
        shutdown(handle, 0);

    }
}

internal struct sockaddr_in6
{
    public ushort sin_family;
    public ushort sin6_port;
    public uint sin6_flowinfo;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public byte[] sin6_addr;
    public uint sin6_scope_id;
}