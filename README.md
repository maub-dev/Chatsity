
# Chatsity | Financial Chat
Browser-based chat application with multiple chatroom support. Allows users to interact with each other and with StooqBot, that will get stock quotes as requested by a specific command.


## Mandatory Features
- **Chat with others**: Allow registered users to log in and talk with other users in a chatroom.
- **Command to get stock quotes**: Allow users to post messages as commands into the chatroom with the following format /stock=stock_code
- **Decoupled bot**: Create a decoupled bot that will call an API using the stock_code as a parameter (https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv, here aapl.us is the stock_code)
- **CSV File**: The bot should parse the received CSV file and then it should send a message back into the chatroom using a message broker like RabbitMQ. The message will be a stock quote using the following format: “APPL.US quote is $93.42 per share”. The post owner will be the bot.
- **Chat messages**: Have the chat messages ordered by their timestamps and show only the last 50 messages.
- **Unit testing**: Unit test the functionality you prefer. **NOT IMPLEMENTED**


## Bonus (Optional)
- **Multiple chatrooms**: Have more than one chatroom.
- **.NET Identity**: Use .NET identity for users authentication.
- **Invalid messages**: Handle messages that are not understood or any exceptions raised within the bot. **NOT IMPLEMENTED**
- **Installer**: Build an installer. **NOT IMPLEMENTED**


## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.