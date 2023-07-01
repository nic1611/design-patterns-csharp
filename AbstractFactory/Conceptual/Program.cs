namespace AbstractFactory.Conceptual;

// The Abstract Factory interface declares a set of methods that return
// different abstract products. These products are called a family and are
// related by a high-level theme or concept. Products of one family are
// usually able to collaborate among themselves. A family of products may
// have several variants, but the products of one variant are incompatible
// with products of another.
public interface IAbstractFactory
{
    IAbstractChair CreateChair();
    IAbstractTable CreateTable();
}

// Concrete Factories produce a family of products that belong to a single
// variant. The factory guarantees that resulting products are compatible.
// Note that signatures of the Concrete Factory's methods return an abstract
// product, while inside the method a concrete product is instantiated.
class ConcreteFactory1 : IAbstractFactory
{
    public IAbstractChair CreateChair()
    {
        return new ConcreteModernChair();
    }
    public IAbstractTable CreateTable()
    {
        return new ConcreteModernTable();
    }
}

// Each Concrete Factory has a corresponding product variant.
class ConcreteFactory2 : IAbstractFactory
{
    public IAbstractChair CreateChair()
    {
        return new ConcreteVictorianChair();
    }
    public IAbstractTable CreateTable()
    {
        return new ConcreteVictorianTable();
    }
}

// Each distinct product of a product family should have a base interface.
// All variants of the product must implement this interface.
public interface IAbstractChair
{
    string UsefulFunctionChair();
}

// Concrete Products are created by corresponding Concrete Factories.
class ConcreteModernChair : IAbstractChair
{
    public string UsefulFunctionChair()
    {
        return "The result of the ModernChair.";
    }
}

class ConcreteVictorianChair : IAbstractChair
{
    public string UsefulFunctionChair()
    {
        return "The result of the VictorianChair";
    }
}

// Here's the the base interface of another product. All products can
// interact with each other, but proper interaction is possible only between
// products of the same concrete variant.
public interface IAbstractTable
{
    // Table is able to do its own thing...
    string UsefulFunctionTable();
    // ...but it also can collaborate with the ProductA.
    //
    // The Abstract Factory makes sure that all products it creates are of
    // the same variant and thus, compatible.
    string AnotherUsefulFunctionTable(IAbstractChair collaborator);
}

// Concrete Products are created by corresponding Concrete Factories.
class ConcreteModernTable : IAbstractTable
{
    public string UsefulFunctionTable()
    {
        return "The result of the ModerTable.";
    }

    // The variant, Product ModernTable, is only able to work correctly with the
    // variant, ModernChair. Nevertheless, it accepts any instance of
    // AbstractChair as an argument.
    public string AnotherUsefulFunctionTable(IAbstractChair collaborator)
    {
        var result = collaborator.UsefulFunctionChair();
        return $"The result of the ModernTable collaborating with the ({result})";
    }
}

class ConcreteVictorianTable : IAbstractTable
{
    public string UsefulFunctionTable()
    {
        return "The result of the VictorianTable";
    }
    
   // The variant, VictorianTable, is only able to work correctly with the
   // variant, VictorianChair. Nevertheless, it accepts any instance of
   // IAbstractChair as an argument.
    public string AnotherUsefulFunctionTable(IAbstractChair collaborator)
    {
        var result = collaborator.UsefulFunctionChair();
        return $"The result of the VictorianTable collaborating with the ({result})";
    }
}

// The client code works with factories and products only through abstract
// types: AbstractFactory and AbstractProduct. This lets you pass any
// factory or product subclass to the client code without breaking it.
class Client
{
    public void Main()
    {
        // The client code can work with any concrete factory class.
        Console.WriteLine("Client: Testing client code with the first factory type...");
        ClientMethod(new ConcreteFactory1());
        Console.WriteLine();
        Console.WriteLine("Client: Testing the same client code with the second factory type...");
        ClientMethod(new ConcreteFactory2());
    }
    public void ClientMethod(IAbstractFactory factory)
    {
        var chair = factory.CreateChair();
        var table = factory.CreateTable();
        Console.WriteLine(table.UsefulFunctionTable());
        Console.WriteLine(table.AnotherUsefulFunctionTable(chair));
    }
}

public class Program
{
    public Program()
    {
        new Client().Main();
    }
}
