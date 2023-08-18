﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RoadSaintsAPI.Models;
using RoadSaintsAPI.Repository;

namespace RoadSaintsAPI.Controllers
{
    [RoutePrefix("api/wishlist")]
    public class WishlistController : ApiController
    {
        [HttpPost]
        [Route("addtowishlist")]
        public IHttpActionResult AddToWishlist([FromBody] WishlistModel wishlistItem)
        {
            WishlistRepo wishlistRepo = new WishlistRepo();

            if (wishlistItem == null)
            {
                return BadRequest("Wishlist item data is null.");
            }

            bool isAdded = wishlistRepo.AddToWishlist(wishlistItem);

            if (isAdded)
            {
                return Ok("Item added to wishlist successfully.");
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("allwishlistitems")]
        public IHttpActionResult GetAllWishlistItems()
        {
            WishlistRepo wishlistRepo = new WishlistRepo();

            List<WishlistModel> wishlistItems = wishlistRepo.GetAllWishlistItems();
            if (wishlistItems == null || wishlistItems.Count == 0)
            {
                return NotFound();
            }
            return Ok(wishlistItems);
        }

        [HttpGet]
        [Route("details/{wishlistItemId}")]
        public IHttpActionResult GetWishlistItemById(int wishlistItemId)
        {
            WishlistRepo wishlistRepo = new WishlistRepo();
            WishlistModel wishlistItem = wishlistRepo.GetWishlistItemById(wishlistItemId);
            if (wishlistItem == null)
            {
                return NotFound();
            }
            return Ok(wishlistItem);
        }

        [HttpDelete]
        [Route("deletewishlistitem/{wishlistItemId}")]
        public IHttpActionResult DeleteWishlistItemById(int wishlistItemId)
        {
            WishlistRepo wishlistRepo = new WishlistRepo();

            bool isDeleted = wishlistRepo.DeleteWishlistItemById(wishlistItemId);

            if (isDeleted)
            {
                return Ok("Item removed from wishlist successfully.");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("deletewishlistitemsbycustomer/{customerId}")]
        public IHttpActionResult DeleteWishlistItemsByCustomerId(int customerId)
        {
            WishlistRepo wishlistRepo = new WishlistRepo();
            bool isDeleted = wishlistRepo.DeleteWishlistItemsByCustomerId(customerId);

            if (isDeleted)
            {
                return Ok("Wishlist items removed successfully.");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
