from playwright.sync_api import Page, expect
from helpers.helper import click_if_visible

class IceProfilePage:

    def __init__(self, page: Page):
        self.page = page
        self.header = page.locator('.logoResponsive')
        self.user_drop_down = page.locator('.dropdown.user-menu')
        self.sign_out_button = page.get_by_role('link', name='Sign out')
        self.cancel_button = page.locator('button.cancelBtn')
    
    def check_header(self):
        self.header.is_visible()
        # expect(self.header).to_contain_text()

    def log_out(self):
        # self.page.wait_for_timeout(5000)
        click_if_visible(self.cancel_button)
        self.user_drop_down.is_visible()
        self.user_drop_down.click()
        self.sign_out_button.is_visible()
        self.sign_out_button.click()
        
