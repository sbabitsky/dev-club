namespace dev.club.solid
{
    /// <summary>
    /// // SRP: The Single-Responsibility Principle
    /// </summary>
    public class SingleResponsibility
    {
        public string Statement => "A class should have only one reason to change.";
        public string Why => @"Because each responsibility has its own axis of change. When the requirements change,
                              the appropriate change must be applied among the classes. Less responsibilities == less updated needed.";
    }

    /// <summary>
    /// OCP: The Open-Closed Principle
    /// </summary>
    public class TheOpenClosedPrinciple
    {
        public string Statement => @"Software entities (classes, modules, functions, etc) should be open for extension,
                                    but closed for modification.";
        public string Why => @"Sometimes single change to a program results in a cascade of changes to dependent modules.
                               Modifications and further changes should be achieved by adding new code, not by
                               changing old code that already works.";
    }

    /// <summary>
    /// LSP: The Liskov Substitution Principle
    /// </summary>
    public class TheLiskovSubstitutionPrinciple
    {
        public string Statement => "Subtypes must be substitutable for their base types.";
        public string Why => @"Violating the LSP principles often results in the run-time exceptions and
                               changes in the code that already works.";
    }

    /// <summary>
    /// DIP: The Dependency-Inversion Principle
    /// </summary>
    public class TheDependencyInversionPrinciple
    {
        public string StatementA => "High-level modules should not depend on low-level modules. Bot should depend on abstraction.";
        public string StatementB => "Abstractions should not depend on the details. Details should depend on abstractions.";
        public string Why => "The changes in the lower level modules have direct affect on the higher level modules.";
    }

    /// <summary>
    /// ISP: The Interface-Segregation Principle
    /// </summary>
    public class TheInterfaceSegregationPrinciple
    {
        public string Statement => "Clients should not be forced to depend on methods that they do not use";

        public string Why => @"When clients are forced to depend on methods that they don't use, then those clients
                               are subject to changes to those methods. This results in coupling between all the clients";
    }
}
