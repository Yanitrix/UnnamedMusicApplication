using Networking.Messages.Client;
using System;
using System.Collections.Generic;

namespace Networking
{
    public struct ClientPermissions
    {
        private readonly HashSet<Type> allowedTypes;
        private readonly short id;

        private ClientPermissions(short id, params Type[] allowedTypes)
        {
            this.id = id;
            this.allowedTypes = new HashSet<Type>(allowedTypes);
        }

        private ClientPermissions(short id, HashSet<Type> allowedTypes)
        {
            this.id = id;
            this.allowedTypes = allowedTypes;
        }

        public bool IsAllowed(Type messageType) => allowedTypes.Contains(messageType);

        public static ClientPermissions Display { get; } = new ClientPermissions(0, typeof(ListPlaylistsRequest), typeof(PlaylistDetailsRequest));

        public static ClientPermissions Suggest { get; } = new ClientPermissions(1, typeof(SuggestionMessage));

        public static ClientPermissions Play { get; } = new ClientPermissions(
            2,
            typeof(MoveQueueMessage), typeof(SetShuffleMessage), typeof(InsertSongsMessage),
            typeof(ReplaceQueueMessage), typeof(AddSongsMessage), typeof(ClearQueueMessage),
            typeof(ResetQueueMessage), typeof(SetCurrentSongMessage)
            );

        public static ClientPermissions Modify { get; } = new ClientPermissions(4, typeof(UpdatePlaylistMessage));

        public static implicit operator ClientPermissions(short id)
        {
            if (id < 0 || id > 7)
                return new(id, new HashSet<Type>());
            if (id == 0)
                return Display;
            if (id == 1)
                return Suggest;
            if (id == 2)
                return Play;
            if (id == 4)
                return Modify;

            //from 0 to 4
            var pows = PowsOfTwo(id);
            ClientPermissions sum = new(0);

            foreach(int i in pows)
            {
                ClientPermissions current = (short)i;
                sum |= current;
            }

            return sum;
        }

        public static implicit operator short(ClientPermissions permissions) => permissions.id;

        public static ClientPermissions operator |(ClientPermissions left, ClientPermissions right)
        {
            var set = new HashSet<Type>(left.allowedTypes);
            set.UnionWith(right.allowedTypes);
            return new((short)(left.id + right.id), set);
        }

        private static int[] PowsOfTwo(int sum)
        {
            Span<int> pows = stackalloc int[4];

            for(int i = 0; sum > 0; i++)
            {
                pows[i] = sum % 2;
                sum /= 2;
            }

            for(int i = 0; i < pows.Length; i++)
            {
                pows[i] = (int)Math.Pow(2 , pows[i]);
            }

            return pows.ToArray();
        }
    }
}