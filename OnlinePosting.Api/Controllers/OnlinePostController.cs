using Microsoft.AspNetCore.Mvc;
using OnlinePosting.Application.Implementation.Commands.CreatePost;
using OnlinePosting.Application.Implementation.Commands.DeletePost;
using OnlinePosting.Application.Implementation.Commands.UpdatePost;
using OnlinePosting.Application.Implementation.Queries.GetPosts;
using OnlinePosting.Application.Implementation.Queries.GetSinglePost;
using OnlinePosting.Application.Shared.Dto.Request;

namespace OnlinePosting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlinePostController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetOnlinePosts()
        {
            var posts = await Mediator.Send(new GetPostEntityQuery());

            if (posts == null || posts.Count <  0) { return NotFound("No post found");}
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSinglePostById(int id)
        {
            var post = await Mediator.Send(new GetPostEntityByIdQuery() { PostId = id });

            if (post == null) { return NotFound("Post does not exist"); }
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPost([FromBody] PostEntityRequestDto requestDto)
        {
            var command = new CreatePostEntityCommand(requestDto);
            var createdPost = await Mediator.Send(command);

            if (createdPost == null) { return BadRequest("Post not created"); }
            return CreatedAtAction(nameof(GetSinglePostById), new { id = createdPost.Id }, createdPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdateEntityRequestDto updateEntity)
        {
            var command = new UpdatePostEntityCommand(updateEntity);
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var delete = await Mediator.Send(new DeletePostEntityCommand() { Id = id });

            if (delete == 0) { return NotFound("No post deleted"); }
            return NoContent();
        }
    }
}
