package com.yourname.selenium.pages;

import org.openqa.selenium.StaleElementReferenceException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.openqa.selenium.support.ui.ExpectedConditions;
import java.time.Duration;
import java.util.List;

public class CartPage {
    WebDriver driver;

    // Constructor
    public CartPage(WebDriver driver) {
        this.driver = driver;
        PageFactory.initElements(driver, this);
    }

    // Elements
    @FindBy(css = "span.title")
    WebElement title;

    @FindBy(css = "button[id*='remove']")
    WebElement removeButton;

    @FindBy(css = "span.shopping_cart_badge")
    List <WebElement> cartBadge;

    @FindBy(css = "div.inventory_item_name ")
    List <WebElement> product;

    @FindBy(css = "button#continue-shopping")
    WebElement continueShoppingButton;

    @FindBy(css = "button#checkout")
    WebElement checkoutButton;

    public void checkCartTitle(String cartTitle) {
        new WebDriverWait(driver, Duration.ofSeconds(5))  // wait up to 5 seconds
            .until(ExpectedConditions.visibilityOf(title));

        String actualTitle = title.getText();
        String expectedTitle = cartTitle;

        actualTitle.equals(expectedTitle);
        }

    public void checkProductName(String productName) {
        String cartProductName = product.get(0).getText();
        System.out.println("Product in cart: " + cartProductName);

        cartProductName.equals(productName);
    }

    public boolean areButtonsVisible() {
        return continueShoppingButton.isDisplayed() && checkoutButton.isDisplayed();
    }

    public void removeFromCart() {
        removeButton.click();
    }

    public boolean cartBageNotVisible() {
        try {
            // Wait for the cart badge to become stale (removed or updated in the DOM)
            WebDriverWait wait = new WebDriverWait(driver, Duration.ofSeconds(10));
            wait.until(ExpectedConditions.stalenessOf(cartBadge.get(0))); // Wait until the badge element is stale

            // Check if the cartBadge list is empty (badge is not present in the DOM)
            if (cartBadge.isEmpty()) {
                // System.out.println("Cart badge is not present in the DOM.");
                return true;
            }

            // If the list is not empty, check if the badge is visible
            // System.out.println("Cart badge exists, checking visibility...");
            return !cartBadge.get(0).isDisplayed();
        } catch (StaleElementReferenceException e) {
            // If the element reference is stale (badge has been removed), assume it's not visible
            // System.out.println("StaleElementReferenceException: Badge reference is stale.");
            return true;
        } catch (Exception e) {
            // Handle any other exceptions (optional)
            // e.printStackTrace();
            return false;
        }
    }

    public void continueInShopping() {
        continueShoppingButton.click();
    }
}
