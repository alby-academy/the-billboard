﻿namespace TheBillboard.API.Domain;
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

    public Message(
        int? id,
        string title,
        string body,
        int authorId,
        DateTime? createdAt,
        DateTime? updatedAt
        ) : this(title, body, authorId, default, createdAt, updatedAt, id)
    {
    }

    public Message(int id, string title, string body, int authorId, string name, string surname, string email, DateTime messageCreatedAt, DateTime messageUpdatedAt, DateTime authorCreatedAt)
    {
        Id = id;
        Title = title;
        Body = body;
        AuthorId = authorId;
        CreatedAt = messageCreatedAt;
        UpdatedAt = messageUpdatedAt;
        Author = new Author(name, surname, authorId, email, CreatedAt: authorCreatedAt);
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
//public record Message(
//                   int? Id = default,
//                   string Title = "",
//                   string Body = "",
//                   DateTime? MessageCreatedAt = default,
//                   int AuthorId = default,
//                   DateTime? MessageUpdatedAt = default,
//                   string Name = "",
//                   string Surname = "",
//                   string Mail = "",
//                   DateTime? AuthorCreatedAt = default
//                   )
//{    
//    public Author Author => new Author(AuthorId, Name, Surname,Mail,AuthorCreatedAt);
//    public string FormattedCreatedAt => MessageCreatedAt.HasValue
//        ? MessageCreatedAt.Value.ToString("yyyy-MM-dd HH:mm")
//        : string.Empty;

//    public string FormattedUpdatedAt => MessageUpdatedAt.HasValue
//        ? MessageUpdatedAt.Value.ToString("yyyy-MM-dd HH:mm")
//        : string.Empty;
//}

// insert into message(...) output inserted.id values(...)
