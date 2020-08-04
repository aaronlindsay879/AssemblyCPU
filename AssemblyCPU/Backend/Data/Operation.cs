using System.ComponentModel;
using System.Reflection;

namespace AssemblyCPU.Backend.Data
{
    public enum Operation
    {
        [Category("Memory")] LDR = 1,
        [Category("Memory")] STR,
        [Category("Arithmetic")] ADD,
        [Category("Arithmetic")] SUB,
        [Category("Arithmetic")] MOV,
        [Category("Arithmetic")] CMP,
        [Category("Branch")] B,
        [Category("Branch")] BEQ,
        [Category("Branch")] BNE,
        [Category("Branch")] BGT,
        [Category("Branch")] BLT,
        [Category("Bitwise")] AND,
        [Category("Bitwise")] ORR,
        [Category("Bitwise")] EOR,
        [Category("Bitwise")] MVN,
        [Category("Bitwise")] LSL,
        [Category("Bitwise")] LSR,
        [Category("Other")] HALT
    }
    public static class Operations
    {
        public static string GetCategory(this Operation source)
        {
            FieldInfo fieldInfo = source.GetType().GetField(source.ToString());
            CategoryAttribute attribute = (CategoryAttribute)fieldInfo.GetCustomAttribute(typeof(CategoryAttribute), false);

            return attribute.Category;
        }
    }
}
