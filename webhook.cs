[HttpPost("webhook")]
public async Task<IActionResult> StripeWebhook()
{
    var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
    var endpointSecret = _config["Stripe:WebhookSecret"];

    Event stripeEvent;

    try
    {
        stripeEvent = EventUtility.ConstructEvent(json,
            Request.Headers["Stripe-Signature"], endpointSecret);
    }
    catch (Exception e)
    {
        return BadRequest($"Webhook Error: {e.Message}");
    }

    // Handle successful payment
    if (stripeEvent.Type == Events.CheckoutSessionCompleted)
    {
        var session = stripeEvent.Data.Object as Session;

        // Optional: Retrieve order ID from metadata (you must store it when creating the session)
        var orderId = int.Parse(session.Metadata["order_id"]);

        var order = await _context.Orders.FindAsync(orderId);
        if (order != null)
        {
            order.Status = "Paid";
            await _context.SaveChangesAsync();
        }
    }

    return Ok();
}
