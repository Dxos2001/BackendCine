using System;
using BackendCine.Entities.Models;
using Company.TestProject1;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity.Data;
using BackendCine.Entities;

namespace BackendCine.Services;

public class UsuarioService
{
    private readonly CineDbContext _context;
    public UsuarioService(CineDbContext context)
    {
        _context = context;
    }

    public async Task<List<Usuario>> GetUsuariosAsync()
    {
        return await _context.Usuarios.Where(u => u.swt).ToListAsync();
    }

    public async Task<Usuario> GetUsuarioByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task AddUsuarioAsync(Usuario usuario)
    {
        usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario)
    {
        var existingUsuario = await GetUsuarioByIdAsync(usuario.Id);
        if (existingUsuario == null)
        {
            throw new KeyNotFoundException($"Usuario with ID {usuario.Id} not found.");
        }
        existingUsuario.NomUsuario = usuario.NomUsuario;
        existingUsuario.Password = usuario.Password;
        existingUsuario.Email = usuario.Email;
        existingUsuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
        existingUsuario.IdCliente = usuario.IdCliente;
        existingUsuario.usuario_modificacion = usuario.usuario_modificacion;
        existingUsuario.fecha_modificacion = DateTime.Now;
        _context.Usuarios.Update(existingUsuario);
        await _context.SaveChangesAsync();
        return existingUsuario;
    }
    public async Task<Usuario> DeleteUsuarioAsync(int id)
    {
        var usuario = await GetUsuarioByIdAsync(id);
        if (usuario != null)
        {
            usuario.swt = false;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException($"Usuario with ID {id} not found.");
        }
        return usuario;
    }
    
    public async Task<Usuario> AuthenticateAsync(LoginParam param)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NomUsuario == param.NomUsuario && u.swt);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(param.Password, usuario.Password))
        {
            return null; // Authentication failed
        }
        return usuario; // Authentication successful
    }

}
