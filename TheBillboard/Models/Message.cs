﻿namespace TheBillboard.MVC.Models;

using System.ComponentModel.DataAnnotations;

public record Message
{
    public Message(
        string title = "",
        [Required(ErrorMessage = "Il campo Message e' obbligatorio")] [MinLength(5, ErrorMessage = "Il campo Message deve essere lungo almento 5 caratteri")]
        string body = "",
        int authorId = default,
        Author? author = default,
        DateTime? createdAt = default,
        DateTime? updatedAt = default,
        int? id = default)
    {
        Title = title;
        Body = body;
        AuthorId = authorId;
        Author = author;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Id = id;
    }

    // (System.Int32 Id,
    // System.String Title,
    // System.String Body,
    //
    // System.DateTime MessageCreatedAt,
    // System.Int32 AuthorId, System.DateTime MessageUpdatedAt, System.String Name, System.String Surname, System.String Email, System.DateTime AuthorCreatedAt)
    public Message(int id, string title, string body, int authorId, string name, string surname, string email, DateTime messageCreatedAt, DateTime messageUpdatedAt, DateTime authorCreatedAt)
    {
        Id = id;
        Title = title;
        Body = body;
        AuthorId = authorId;
        CreatedAt = messageCreatedAt;
        UpdatedAt = messageUpdatedAt;
        Author = new Author(name, surname, authorId, email, CreatedAt: authorCreatedAt);

        // var a = new Author("A", "V", UpdatedAt: DateTime.Now);
    }

    public string Title { get; init; }
    public string Body { get; init; }
    public int AuthorId { get; init; }
    public Author? Author { get; init; }
    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public int? Id { get; init; }

    public string FormattedCreatedAt => CreatedAt.HasValue
        ? CreatedAt.Value.ToString("yyyy-MM-dd HH:mm")
        : string.Empty;

    public string FormattedUpdatedAt => UpdatedAt.HasValue
        ? UpdatedAt.Value.ToString("yyyy-MM-dd HH:mm")
        : string.Empty;
}