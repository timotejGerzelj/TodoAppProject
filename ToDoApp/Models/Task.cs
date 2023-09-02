namespace ToDoApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("todo_tasks")]
public class Task 
{
    [Column("id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Naslov je obvezen.")]
    [Column("naslov")]
    public required string Naslov { get; set; }

    [Required(ErrorMessage = "Opis je obvezen.")]
    [Column("opis")]
    public required string Opis { get; set; } 

    [Required(ErrorMessage = "DatumUstvarjanja polje je obvezno.")]
    [Column("datum_ustvarjanja")]
    public required DateTime DatumUstvarjanja { get; set;}

    [Required(ErrorMessage = "Vrednost Opravljeno je obvezna.")]
    [Column("opravljeno")]
    public required bool Opravljeno { get; set; }
}