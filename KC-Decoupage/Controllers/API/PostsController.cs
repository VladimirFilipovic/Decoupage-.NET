using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using KC_Decoupage.Dtos;
using KC_Decoupage.Models;

namespace KC_Decoupage.Controllers.API
{
    public class PostsController : ApiController
    {

        private ApplicationDbContext _context;

        public PostsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET /api/posts
        public IHttpActionResult GetPosts()
        {
           var postDtos = _context.Post.ToList().Select(Mapper.Map<Post, PostDto>); //delegate

            return Ok(postDtos);
        }
        //get /api/posts/1
        public IHttpActionResult GetPost(int id)
        {
            var post = _context.Post.SingleOrDefault(p => p.Id == id);

            if (post == null)
                return NotFound();
            
            return Ok(Mapper.Map<Post,PostDto> (post));
        }

        //POST /api/posts
        [System.Web.Http.HttpPost] //because we are creeating a resourse
        public IHttpActionResult CreatePost(PostDto postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var post = Mapper.Map<PostDto, Post>(postDto);
            
            _context.Post.Add(post);
            _context.SaveChanges();

            postDto.Id = post.Id;

            return Created(new Uri(Request.RequestUri + "/" + post.Id),postDto);
        }

        //PUT /api/posts/1
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdatePost(int id, PostDto postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
           
            var postInDb = _context.Post.SingleOrDefault(p => p.Id == id);

            if (postInDb == null)
                return NotFound();

            Mapper.Map(postDto, postInDb);


            _context.SaveChanges();

            return Ok();

        }



        //Delete /api/posts/1
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeletePost(int id)
        {
            var postInDb = _context.Post.SingleOrDefault(p => p.Id == id);

            if (postInDb == null)
                return NotFound();

            _context.Post.Remove(postInDb);
            _context.SaveChanges();

            return Ok();
        }
        
    }
}
