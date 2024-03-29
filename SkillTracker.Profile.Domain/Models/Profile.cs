﻿using System.ComponentModel.DataAnnotations;

namespace SkillTracker.Profile.Domain.Models;

public class Profile
{
    [Key] public string AssociateId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public DateTime? AddedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public List<Skill> Skills { get; set; }
}