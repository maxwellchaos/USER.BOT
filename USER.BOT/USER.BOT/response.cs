﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace USER.BOT
{
    public class CaptchaGet
    {
        public Error error { get; set; }


        public class RequestParam
        {
            public string key { get; set; }
            public string value { get; set; }
        }
	}


    public class Cheak
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
            public int admin_author_id { get; set; }
            public bool is_hidden { get; set; }
        }

        public class Response
        {
            public int count { get; set; }
            public List<Item> items { get; set; }
        }

    }
    public class MessagesGet
    {
        public List<Response> response { get; set; }
        public class Response
        {
            public string key { get; set; }
            public string value { get; set; }
        }

    }
    public class MessagesNew
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
        }

    }
    public class BanFriends
    {
        public List<ResponseBanFriends> response { get; set; }
        public class ResponseBanFriends
        {
            public string first_name { get; set; }
            public int id { get; set; }
            public string last_name { get; set; }
            public bool can_access_closed { get; set; }
            public bool is_closed { get; set; }
            public string deactivated { get; set; }
        }

    }
    public class FriendsGet
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
        }

        public class Response
        {
            public int count { get; set; }
            public List<Item> items { get; set; }
        }

    }




    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


    public class UsersGet
    {
        public List<Response> response { get; set; }
        public class Response
        {
            public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public bool is_closed { get; set; }
            public bool can_access_closed { get; set; }
            public string photo_50 { get; set; }
            public string photo_100 { get; set; }
        }
    }




    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class GetProfileInfo
    {
        public Response response { get; set; }
        public class City
        {
            public int id { get; set; }
            public string title { get; set; }
        }

        public class Country
        {
            public int id { get; set; }
            public string title { get; set; }
        }

        public class Response
        {
            public string first_name { get; set; }
            public int id { get; set; }
            public string last_name { get; set; }
            public string home_town { get; set; }
            public string status { get; set; }
            public string bdate { get; set; }
            public int bdate_visibility { get; set; }
            public City city { get; set; }
            public Country country { get; set; }
            public string phone { get; set; }
            public int relation { get; set; }
            public int sex { get; set; }
        }
    }


    public class FriendsGet1
    {
        public Response response { get; set; }
        public class Item
        {
            public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public bool is_closed { get; set; }
            public bool can_access_closed { get; set; }
            public int can_post { get; set; }
            public int online { get; set; }
            public string track_code { get; set; }
            public List<int> lists { get; set; }
            public string deactivated { get; set; }
            public string bdate { get; set; }
            public int sex { get; set; }
        }
        public class Response
        {
            public int count { get; set; }
            public List<Item> items { get; set; }
        }

    }
    public class WallGet
    {
        public class Size
        {
            public int height { get; set; }
            public string url { get; set; }
            public string type { get; set; }
            public int width { get; set; }
        }

        public class Photo
        {
            public int album_id { get; set; }
            public int date { get; set; }
            public int id { get; set; }
            public int owner_id { get; set; }
            public bool has_tags { get; set; }
            public double lat { get; set; }
            public double @long { get; set; }
            public int post_id { get; set; }
            public List<Size> sizes { get; set; }
            public string text { get; set; }
        }

        public class Attachment
        {
            public string type { get; set; }
            public Photo photo { get; set; }
        }


        public class PostSource
        {
            public string type { get; set; }
            public string platform { get; set; }
            public string data { get; set; }
        }

        public class Comments
        {
            public int count { get; set; }
            public int can_post { get; set; }
            public bool groups_can_post { get; set; }
            public int can_close { get; set; }
        }

        public class Likes
        {
            public int count { get; set; }
            public int user_likes { get; set; }
            public int can_like { get; set; }
            public int can_publish { get; set; }
        }

        public class Reposts
        {
            public int count { get; set; }
            public int wall_count { get; set; }
            public int mail_count { get; set; }
            public int user_reposted { get; set; }
        }
        public class Views
        {
            public int count { get; set; }
        }

        public class Item
        {
            public int id { get; set; }
            public int from_id { get; set; }
            public int owner_id { get; set; }
            public int date { get; set; }
            public string post_type { get; set; }
            public string text { get; set; }
            public int can_delete { get; set; }

            public int can_pin { get; set; }
            public bool can_archive { get; set; }
            public bool is_archived { get; set; }
            public List<Attachment> attachments { get; set; }

            public PostSource post_source { get; set; }
            public Comments comments { get; set; }
            public Likes likes { get; set; }
            public Reposts reposts { get; set; }

            public Views views { get; set; }
            public int? edited { get; set; }

        }

        public class Response
        {
            public int count { get; set; }
            public List<Item> items { get; set; }
        }

        public Response response { get; set; }
    }


       





    public class Err_main
    {
        public Error error { get; set; }
        public class RequestParam
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        public class Error
        {
            public int error_code { get; set; }
            public string error_msg { get; set; }
            public List<RequestParam> request_params { get; set; }
            public string captcha_sid { get; set; }
            public string captcha_img { get; set; }
        }
    }



    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class CommentsGet
    {
        public Response response { get; set; }

        public class Thread
        {
            public int count { get; set; }
            public List<object> items { get; set; }
            public bool can_post { get; set; }
            public bool show_reply_button { get; set; }
            public bool groups_can_post { get; set; }
        }

        public class Item
        {
            public int id { get; set; }
            public int from_id { get; set; }
            public int post_id { get; set; }
            public int owner_id { get; set; }
            public List<object> parents_stack { get; set; }
            public int date { get; set; }
            public string text { get; set; }
            public Thread thread { get; set; }
        }

        public class Response
        {
            public int count { get; set; }
            public List<Item> items { get; set; }
            public int current_level_count { get; set; }
            public bool can_post { get; set; }
            public bool show_reply_button { get; set; }
            public bool groups_can_post { get; set; }
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CommentsGet2
    {
        public Response response { get; set; }

        public class Item
        {
            public int id { get; set; }
            public int from_id { get; set; }
            public int post_id { get; set; }
            public int owner_id { get; set; }
            public List<int> parents_stack { get; set; }
            public int date { get; set; }
            public string text { get; set; }
        }

        public class Response
        {
            public int count { get; set; }
            public List<Item> items { get; set; }
            public int current_level_count { get; set; }
            public bool can_post { get; set; }
            public bool show_reply_button { get; set; }
            public bool groups_can_post { get; set; }
        }
    }

    public class messagesGetConversations
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
}
