using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _3abarni_backend.Hubs
{
    public class ChatHub : Hub
    {
        // Maintain a list of connected users
        private static readonly Dictionary<string, string> UserConnections = new Dictionary<string, string>();

        // Method to get the connection ID for a user by username
        public string GetConnectionIdByUsername(string username)
        {
            if (UserConnections.TryGetValue(username, out var connectionId))
            {
                return connectionId;
            }
            return null;
        }

        // Method to associate a username with a connection ID
        public void AddUserConnection(string username)
        {
            var connectionId = Context.ConnectionId;
            Console.WriteLine($"Connection ID for user in connection {username}: {connectionId}");

            if (UserConnections.ContainsKey(username))
            {
                UserConnections[username] = connectionId;
            }
            else
            {
                UserConnections.Add(username, connectionId);
            }
        }

        // Method to send a message to a specific user
        public async Task SendMessageToUser(string receiverUsername, string message)
        {

            if (UserConnections.TryGetValue(receiverUsername, out var receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", Context.ConnectionId, message);
                Console.WriteLine($"Message sent: {message} from {Context.ConnectionId} to {receiverConnectionId}");
            }
            else
            {
                // Handle the case where the receiver's connection ID is not found
                Console.WriteLine($"Error: Receiver with username {receiverUsername} not found.");
            }
        }

    }
}