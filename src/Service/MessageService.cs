namespace TelegramStickerPorter;

public class MessageService
{
    private readonly ILogger<MessageService> _logger;

    public MessageService(ILogger<MessageService> logger)
    {
        _logger = logger;
    }

    public async Task<int> SendMessageAsync(
        Bot bot,
        long chatId,
        string messageText,
        InlineKeyboardMarkup inlineKeyboardMarkup = null,
        ParseMode parseMode = ParseMode.Html,
        ReplyParameters replyParameters = null)
    {
        try
        {
            if (string.IsNullOrEmpty(messageText))
                return 0;

            var sendMessage = await bot.SendMessage(
                chatId,
                messageText,
                parseMode: parseMode,
                replyParameters: replyParameters,
                linkPreviewOptions: new LinkPreviewOptions { IsDisabled = true },
                replyMarkup: inlineKeyboardMarkup
            );

            _logger.LogWarning($"消息Id:{sendMessage.MessageId}发送消息: {messageText}");
            return sendMessage.MessageId;
        }
        catch (Exception ex)
        {
            _logger.LogError($"发送消息失败: {ex.Message}");
            return 0;
        }
    }

    public async Task EditMessageAsync(
        Bot bot,
        long chatId,
        int messageId,
        string messageText,
        InlineKeyboardMarkup inlineKeyboardMarkup = null,
        ParseMode parseMode = ParseMode.Html,
        ReplyParameters replyParameters = null)
    {
        try
        {
            if (string.IsNullOrEmpty(messageText))
                return;

            await bot.EditMessageText(
                chatId,
                messageId,
                messageText,
                parseMode: parseMode,
                linkPreviewOptions: new LinkPreviewOptions { IsDisabled = true },
                replyMarkup: inlineKeyboardMarkup
            );
        }
        catch (Exception ex)
        {
            _logger.LogError($"修改消息失败: {ex.Message}");
        }
    }
}