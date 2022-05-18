using System;
using System.Collections.Generic;
using System.Text;

    public class Obfuscator
    {
        private static readonly byte ByteKey = 203;
        private static readonly int Intkey = 169755316;
        private static readonly ushort ushortKey = 65132;

        public static int intEncoding(int packet)
        {
            return packet ^= Intkey;
        }
        public static int intDecoding(int packet)
        {
            return packet ^= Intkey;
        }

        public static byte byteDecoding(byte packet)
        {
            return packet ^= ByteKey;
        }

        public static byte byteEncoding(byte packet)
        {
            return packet ^= ByteKey;
        }

        public static ushort ushortDecoding(ushort packet)
        {
            return packet ^= ushortKey;
        }

        public static ushort ushortEncoding(ushort packet)
        {
            return packet ^= ushortKey;
        }
        public static void stringEncoding(byte[] segment, int offset, ushort len)
        {
            for (int i = 0; i < len; i++)
            {
                segment[offset + i] ^= ByteKey;
            }
        }
        public static void stringDecoding(byte[] segment, int offset, ushort len)
        {
            for (int i = 0; i < len; i++)
            {
                segment[offset + i] ^= ByteKey;
            }
        }
    }