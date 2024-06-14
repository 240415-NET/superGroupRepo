using Microsoft.EntityFrameworkCore;
using LibrarySystem.API.Models;
using LibrarySystem.API.Services;
using Microsoft.VisualBasic;
using System.Linq;

namespace LibrarySystem.API.Data;

public class CheckoutDataAccess : ICheckoutDataAccess
{
    private readonly LibrarySystemContext _checkoutContext;

    public CheckoutDataAccess(LibrarySystemContext checkoutContextFromBuilder)
    {
        _checkoutContext = checkoutContextFromBuilder;
    }

    public async Task<checkoutDTO> CreateNewCheckoutAsync(checkoutDTO newCheckoutFromService)
    {
        //Get the user object
        User? patron = await _checkoutContext.Users.SingleOrDefaultAsync(u => u.userId == newCheckoutFromService.userId);

        //get the book object
        Book? tome = await _checkoutContext.Books.SingleOrDefaultAsync(b => b.barcode == newCheckoutFromService.bookBarcode);

        Checkout checkoutToAdd = new(newCheckoutFromService, patron, tome);

        _checkoutContext.Add(checkoutToAdd);

        await _checkoutContext.SaveChangesAsync();

        return newCheckoutFromService;
    }

    public async Task<List<Checkout>> GetCheckedOutBooksbyUserIdAsync(Guid userIdFromService)
    {


        return await _checkoutContext.Checkouts //ask our context for the collection of Checkout objects in the database
            .Include(Checkout => Checkout.checkoutUser) //We ask entity framework to also grab the associated User object from the User table
            .Include(Checkout => Checkout.checkoutBook)
            .Where(Checkout => Checkout.checkoutUser.userId == userIdFromService) //We then ask for every Checkout by UserId matches the userIdFromService
            .Where(Checkout => Checkout.status.ToLower() == "out")
            .ToListAsync(); //Finally, we turn those items into a list
    }

    public async Task<string> UpdateCheckinInDBAsync(int barcodeFromServices)
    {
  
        Checkout? checkoutToUpdate = await _checkoutContext.Checkouts
            .SingleOrDefaultAsync(Checkin => Checkin.checkoutBook.barcode == barcodeFromServices);

        checkoutToUpdate.status = "IN";
        await _checkoutContext.SaveChangesAsync();
        return $"Book {barcodeFromServices} check status has been changed to: IN";

    }

}