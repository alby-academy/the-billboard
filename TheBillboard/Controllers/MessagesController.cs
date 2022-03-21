﻿using Microsoft.AspNetCore.Mvc;
using TheBillboard.Abstract;
using TheBillboard.Models;
using TheBillboard.ViewModels;

namespace TheBillboard.Controllers;

public class MessagesController : Controller
{
    private readonly IMessageGateway _messageGateway;
    private readonly IAuthorGateway _authorGateway;
    private readonly ILogger<MessagesController> _logger;

    public MessagesController(IMessageGateway messageGateway, ILogger<MessagesController> logger, IAuthorGateway authorGateway)
    {
        _logger = logger;
        _messageGateway = messageGateway;
        _authorGateway = authorGateway;
    }

    public async Task<IActionResult> Index()
    {
        var messages = await _messageGateway.GetAll();
        var authors = await _authorGateway.GetAll();

        var messagesWithAuthor = messages.Select(message => MatchAuthorToMessage(message, authors));

        var createViewModel = new MessageCreationViewModel(new Message(), authors);
        var indexModel = new MessagesIndexViewModel(createViewModel, messagesWithAuthor);
        return View(indexModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create(int? id)
    {
        var message = id is not null ? await _messageGateway.GetById((int)id)! : new Message();

        if (message is null)
        {
            return View("Error");
        }
        else
        {
            var viewModel = new MessageCreationViewModel(message, await _authorGateway.GetAll());
            return View(viewModel);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Message message)
    {
        if (!ModelState.IsValid)
        {
            return View(new MessageCreationViewModel(message, await _authorGateway.GetAll()));
        }

        if (message.Id == default)
        {
            await _messageGateway.Create(message);
        }
        else
        {
            await _messageGateway.Update(message);
        }

        _logger.LogInformation($"Message received: {message.Title}");
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Detail(int id)
    {
        var message = await _messageGateway.GetById(id)!;
        if (message is null)
        {
            return View("Error");
        }

        var authors = await _authorGateway.GetAll();
        var messageWithAuthor = MatchAuthorToMessage(message, authors);
        return View(messageWithAuthor);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _messageGateway.Delete(id);
        return RedirectToAction("Index");
    }

    private MessageWithAuthor MatchAuthorToMessage(Message message, IEnumerable<Author> authors)
        => new MessageWithAuthor(message, authors.FirstOrDefault(a => a.Id == message.AuthorId, new Author("Unknown Author")));
}