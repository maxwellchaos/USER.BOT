using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace USER.BOT
{




    public class docsgetUploadServer
    {
        public Response response { get; set; }
        public class Response
        {
            public string upload_url { get; set; }
        }
    }

    //============================================================================================================
    public class docsget
    {
        public Response response { get; set; }
 public class Size
    {
        public string src { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string type { get; set; }
    }

    public class Photo
    {
        public List<Size> sizes { get; set; }
    }

    public class Preview
    {
        public Photo photo { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string title { get; set; }
        public int size { get; set; }
        public string ext { get; set; }
        public int date { get; set; }
        public int type { get; set; }
        public string url { get; set; }
        public Preview preview { get; set; }
    }

    public class Response
    {
        public int count { get; set; }
        public List<Item> items { get; set; }
    }
    }


    //================================================================================================
    public class messagesgetConversations
        {
            public Response response { get; set; }
            public class Peer
            {
                public int id { get; set; }
                public string type { get; set; }
                public int local_id { get; set; }
            }

            public class SortId
            {
                public int major_id { get; set; }
                public int minor_id { get; set; }
            }

            public class CanWrite
            {
                public bool allowed { get; set; }
            }

            public class Conversation
            {
                public Peer peer { get; set; }
                public int last_message_id { get; set; }
                public int in_read { get; set; }
                public int out_read { get; set; }
                public SortId sort_id { get; set; }
                public int unread_count { get; set; }
                public bool is_marked_unread { get; set; }
                public bool important { get; set; }
                public bool unanswered { get; set; }
                public CanWrite can_write { get; set; }
            }

            public class LastMessage
            {
                public int date { get; set; }
                public int from_id { get; set; }
                public int id { get; set; }
                public int @out { get; set; }
                public int peer_id { get; set; }
                public string text { get; set; }
                public int conversation_message_id { get; set; }
                public List<object> fwd_messages { get; set; }
                public bool important { get; set; }
                public int random_id { get; set; }
                public List<object> attachments { get; set; }
                public bool is_hidden { get; set; }
            }

            public class Item
            {
                public Conversation conversation { get; set; }
                public LastMessage last_message { get; set; }
            }

            public class Response
            {
                public int count { get; set; }
                public List<Item> items { get; set; }
                public int unread_count { get; set; }
            }
        }


        //========================================================================================
        public class friendsGet
        {
            public Response response { get; set; }
            public class Item
            {
                public int id { get; set; }
                public string first_name { get; set; }
                public string last_name { get; set; }
                public bool is_closed { get; set; }
                public bool can_access_closed { get; set; }
                public string photo_50 { get; set; }
                public int online { get; set; }
                public string track_code { get; set; }
                public List<int> lists { get; set; }
                public string deactivated { get; set; }
            }

            public class Response
            {
                public int count { get; set; }
                public List<Item> items { get; set; }
            }
        }


        public class messagesSend
        {
            public List<Response> response { get; set; }
            public class Response
            {
                public int peer_id { get; set; }
                public int message_id { get; set; }
                public int conversation_message_id { get; set; }
            }

        }


    //========================================================================================================================================

    

    public class messagesgetById
    {
        public Response response { get; set; }

        public class Item
        {
            public int date { get; set; }
            public int from_id { get; set; }
            public int id { get; set; }
            public int @out { get; set; }
            public int peer_id { get; set; }
            public string text { get; set; }
            public int conversation_message_id { get; set; }
            public List<object> fwd_messages { get; set; }
            public bool important { get; set; }
            public int random_id { get; set; }
            public List<object> attachments { get; set; }
            public bool is_hidden { get; set; }
        }

        public class Response
        {
            public int count { get; set; }
            public List<Item> items { get; set; }
        }
    }


    //====================================================================================================================================================
    public class getProfiltinfo
        {
            public Response response { get; set; }

            public class Response
            {
                public string first_name { get; set; }
                public int id { get; set; }
                public string last_name { get; set; }
                public string home_town { get; set; }
                public string status { get; set; }
                public string bdate { get; set; }
                public int bdate_visibility { get; set; }
                public string phone { get; set; }
                public int relation { get; set; }
                public string screen_name { get; set; }
                public int sex { get; set; }
            }
        }



    }

