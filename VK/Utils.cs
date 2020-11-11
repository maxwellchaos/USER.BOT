using MethodButton.VK.Methods;
using MethodButton.VK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MethodButton.VK
{
    public static class AdditionsUtils
    {
        public static WallPost GetMostLikedPost(this Wall api, WallGetArguments args, WallOrder order, out int loadedAmount)
        {
            loadedAmount = 0;
            var loadedPosts = new List<WallPost>(args.Count.Value + 1);
            for (int i = 0; i < args.Count; i += 100)
            {
                args.Offset += i;
                loadedPosts.AddRange(api.Get(args));
                loadedAmount = i;
            }
            switch (order)
            {
                case WallOrder.COMMENTS:
                    return loadedPosts.OrderByDescending(post => post.Comments.Count).First();
                case WallOrder.LIKES:
                    return loadedPosts.OrderByDescending(post => post.Likes.Count).First();
                default:
                    return loadedPosts.OrderByDescending(post => post.Views.Count).First();
            }
            
        }
    }

    public enum WallOrder : int
    {
        VIEWES, LIKES, COMMENTS
    }
}
