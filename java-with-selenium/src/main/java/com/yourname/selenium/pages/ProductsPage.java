package com.yourname.selenium.pages;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.openqa.selenium.support.ui.ExpectedConditions;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.time.Duration;
import java.util.List;
import java.util.ArrayList;;

public class ProductsPage {
    WebDriver driver;

    // Constructor
    public ProductsPage(WebDriver driver) {
        this.driver = driver;
        PageFactory.initElements(driver, this);
    }

    // Elements
    @FindBy(css = "div.header_secondary_container")
    WebElement title;

    @FindBy(css = "button[id*='add']")
    List <WebElement> addToBasket;

    @FindBy(css = "button[id*='remove']")
    WebElement removeButton;

    @FindBy(css = "span.shopping_cart_badge")
    WebElement cartBadge;

    @FindBy(css = "div.inventory_item_name ")
    List <WebElement> product;

    @FindBy(css = "button#react-burger-menu-btn")
    WebElement hamburgerMenu;

    @FindBy(css = "a#logout_sidebar_link")
    WebElement logoutButton;

    @FindBy(css = "a.bm-item.menu-item")
    List <WebElement> menuItem;

    // Optional: Add meaningful check/assertion here
    public boolean isTitleVisible() {
        new WebDriverWait(driver, Duration.ofSeconds(15))
            .until(ExpectedConditions.visibilityOf(title));

        System.out.println("Title is visible: " + title.getText());
        return title.isDisplayed() && addToBasket.get(0).isDisplayed();
    }

    public void addFirstProductToBasket() {
        System.out.println("Number of products available to add to basket: " + addToBasket.size());
        if (addToBasket.size() > 1) {
            addToBasket.get(0).click();
        }
    }

    public boolean isRemoveButtonVisible() {
        new WebDriverWait(driver, Duration.ofSeconds(5))
            .until(ExpectedConditions.visibilityOf(removeButton));

        System.out.println("Remove button is visible: " + removeButton.getText());
        return removeButton.isDisplayed();
    }

    public boolean isCartBadgeVisible() {
        new WebDriverWait(driver, Duration.ofSeconds(5))
            .until(ExpectedConditions.visibilityOf(cartBadge));

        System.out.println("Cart badge is visible: " + cartBadge.getText());
        return cartBadge.isDisplayed();
    }

    public void waitFor(WebElement element) {
        new WebDriverWait(driver, Duration.ofSeconds(10));
    }

    public String getProductText() {
        if (product != null && product.size() > 0) {
            System.out.println("First product text: " + product.get(0).getText());
            String firstProductText = product.get(0).getText();
        try (BufferedWriter writer = new BufferedWriter(new FileWriter("outputProduct.txt", true))) {
            writer.write(firstProductText);
            writer.newLine();
        } catch (IOException e) {
            e.printStackTrace();
        }

            return firstProductText;

        } else {
        System.out.println("No products found.");
        return "";
        }
    }

    public void goToCart() {
        cartBadge.click();
    }

    public void logout() {
        hamburgerMenu.click();
        // System.out.println(getTexts());
        logoutButton.click();
    }

    public List<String> getTexts() {
        List <String> texts = new ArrayList<>();
        for (WebElement item : menuItem) {
            // System.out.println(item.getText());
            texts.add(item.getText());
        }
        return texts;
    }
}
