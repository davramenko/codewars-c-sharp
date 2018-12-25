using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

// Create a simple Class at Runtime

// "C:\Program Files (x86)\MSBuild\14.0\Bin\csc.exe" /debug+ /langversion:6 "017 - Create a simple Class at Runtime.cs"

public static class Kata
{
	static ModuleBuilder moduleBuilder = null;
	public static bool DefineClass(string className, Dictionary<string, Type> properties, ref Type actualType)
	{
		try
		{
			var an = new AssemblyName(className);
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
			if (moduleBuilder == null)
				moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
			TypeBuilder tb = moduleBuilder.DefineType(className,
						TypeAttributes.Public |
						TypeAttributes.Class |
						TypeAttributes.AutoClass |
						TypeAttributes.AnsiClass |
						TypeAttributes.BeforeFieldInit |
						TypeAttributes.AutoLayout,
						null);
			ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
			foreach(KeyValuePair<string, Type> entry in properties)
			{
				FieldBuilder fieldBuilder = tb.DefineField("_" + entry.Key, entry.Value, FieldAttributes.Private);

				PropertyBuilder propertyBuilder = tb.DefineProperty(entry.Key, PropertyAttributes.HasDefault, entry.Value, null);
				MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + entry.Key, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, entry.Value, Type.EmptyTypes);
				ILGenerator getIl = getPropMthdBldr.GetILGenerator();

				getIl.Emit(OpCodes.Ldarg_0);
				getIl.Emit(OpCodes.Ldfld, fieldBuilder);
				getIl.Emit(OpCodes.Ret);

				MethodBuilder setPropMthdBldr = tb.DefineMethod(
					"set_" + entry.Key,
					MethodAttributes.Public |
					MethodAttributes.SpecialName |
					MethodAttributes.HideBySig,
					null, new[] { entry.Value }
				);

				ILGenerator setIl = setPropMthdBldr.GetILGenerator();
				Label modifyProperty = setIl.DefineLabel();
				Label exitSet = setIl.DefineLabel();

				setIl.MarkLabel(modifyProperty);
				setIl.Emit(OpCodes.Ldarg_0);
				setIl.Emit(OpCodes.Ldarg_1);
				setIl.Emit(OpCodes.Stfld, fieldBuilder);

				setIl.Emit(OpCodes.Nop);
				setIl.MarkLabel(exitSet);
				setIl.Emit(OpCodes.Ret);

				propertyBuilder.SetGetMethod(getPropMthdBldr);
				propertyBuilder.SetSetMethod(setPropMthdBldr);
			}
			actualType = tb.CreateType();
		}
		catch
		{
			return false;
		}
		return true;
	}
}

public static class KataTest
{
	public static void Main()
	{
		try
		{
			Random rand = new Random();
			Type myType = typeof(object);
			Dictionary<string, Type> properties;

			// Define first class
			properties = new Dictionary<string, Type> { { "SomeInt", typeof(int) }, { "SomeString", typeof(string) }, { "SomeObject", typeof(object) } };
			Kata.DefineClass("SomeClass", properties, ref myType);
			// Instantiate first class
			dynamic myInstance = Activator.CreateInstance(myType);
			myInstance.SomeObject = myInstance;
			myInstance.SomeString = "Hey there";
			myInstance.SomeInt = 3;
			Console.WriteLine($"{myInstance.SomeObject}: {myInstance.SomeString}, {myInstance.SomeInt}");

			// Define second class
			properties = new Dictionary<string, Type> { { "AnotherObject", typeof(object) }, { "SomeDouble", typeof(double) }, { "AnotherString", typeof(string) } };
			Kata.DefineClass("AnotherClass_N" + rand.Next(100), properties, ref myType);
			// Instantiate second class
			myInstance = Activator.CreateInstance(myType);
			myInstance.AnotherObject = "User: ";
			myInstance.AnotherString = "My lucky number is ";
			myInstance.SomeDouble = 92835768;
			Console.WriteLine($"{myInstance.AnotherObject}: {myInstance.AnotherString} {myInstance.SomeDouble} ");

			// Try to redefine first class
			Console.WriteLine(Kata.DefineClass("SomeClass", properties, ref myType));
		}
		catch (Exception ex)
		{
			Console.WriteLine("Exception: {0}", ex.ToString());
			Environment.Exit(1);
		}
	}
}