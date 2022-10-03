using System;
using ApplicationAPI.Data.Models;


namespace ApplicationClient.Utils.Stores;
public sealed class UserStore
{
	private User _currentUser;

	public event EventHandler? CurrentViewModelChanged;

	public User CurrentUser 
	{
		get => _currentUser;
		set
		{
			_currentUser = value;
			OnCurrentUserChanged();
		}
	}

	private void OnCurrentUserChanged() 
	{
		CurrentViewModelChanged?.Invoke(this, EventArgs.Empty);
	}
}