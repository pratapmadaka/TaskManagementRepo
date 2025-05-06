namespace TaskManager.Application.DTOs.Auth;


public record RegisterRequest(String Email, String FullName, String Password, String TenentID);