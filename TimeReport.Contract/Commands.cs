namespace TimeReport.Contract;

public record CreatePersonCommand(string? Name, string? Email); 
//public class CreatePersonCommand
//{
//    public string? Name { get; set; }
//    public string? Email { get; set; }
//}

public record UpdatePersonCommand(int Id, string? Name, string? Email);
//public class UpdatePersonCommand
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public string Email { get; set; }
//}

public record DeletePersonCommand(int Id);
//public class DeletePersonCommand
//{
//    public int Id { get; set; }
//}

