using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocoCompiler2
{
    class TokenEntity
    {
        private int id;
        private int line;
        private int col;
        private string kind;
        private string value;

        public int Line { get => line; set => line = value; }
        public int Col { get => col; set => col = value; }
        public string Kind { get => kind; set => kind = value; }
        public string Value { get => value; set => this.value = value; }
        public int Id { get => id; set => id = value; }

        public TokenEntity(int id, int line, int col, string kind, string value)
        {
            this.line = line;
            this.col = col;
            this.kind = kind;
            this.value = value;
            this.id = id;
        }

        public override string ToString()
        {
            return "Token #" + id + " at (" + line + ", " + col + ") | Kind: " + kind + " | Value: " + value;
        }
    }
}
