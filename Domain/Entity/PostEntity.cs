﻿using Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class PostEntity : BaseEntity
    {
        public string Text { get; set; }

        [Column(Order = 1)]
        public int UserId { get; set; }

        #region Related data

        public ICollection<CommentEntity> Comments { get; set; }

        public UserEntity User { get; set; }

        #endregion Related data
    }
}