using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Controllers;
using NewsApp.Dto;
using NewsApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Tests.Controller
{
    public class CommentControllerTest
    {
        private CommentController _commentController;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentControllerTest()
        {
            //Dependencies
            _postRepository = A.Fake<IPostRepository>();
            _mapper = A.Fake<IMapper>();
            _commentRepository = A.Fake<ICommentRepository>();

            //SUT
            _commentController = new CommentController(_commentRepository, _postRepository, _mapper);
        }

        [Fact]
        public async void PostController_GetCommentsAsync_ReturnOk()
        {
            //Arrange
            var comments = A.Fake<ICollection<CommentOutputDto>>();
            var commentList = A.Fake<List<CommentOutputDto>>();
            A.CallTo(() => _mapper.Map<List<CommentOutputDto>>(comments)).Returns(commentList);

            //Act
            var result = await _commentController.GetCommentsAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
