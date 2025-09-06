import sys
import os
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

import re
from playwright.sync_api import Playwright, sync_playwright, expect,Page
from dotenv import load_dotenv
from pages.ice_login_page import IceLoginPage
from pages.ice_home_page import IceHomePage
from pages.ice_profile_page import IceProfilePage

load_dotenv()

URL = os.getenv("ICE_URL")
USERNAME = os.getenv("ICE_USERNAME")
PASSWORD = os.getenv("ICE_PASSWORD")

# def test_login(page: Page):
def test_ice(page):
    ice_login = IceLoginPage(page)
    ice_home = IceHomePage(page)
    ice_profile = IceProfilePage(page)

    # username = 'admin'
    # password = 'admin'

    # url = 'https://icehrmpro.gamonoid.com/login.php?'

    ice_login.go_to(URL)
    ice_login.check_login_elements()
    ice_login.enter_user_name(USERNAME)
    ice_login.enter_password(PASSWORD)
    ice_login.click_login_button()

    ice_home.check_header()
    new_name = ice_home.get_user()
    print(new_name + USERNAME)
    ice_home.go_to_my_info()

    ice_profile.check_header()
    ice_profile.log_out()

    ice_login.check_login_elements()

    page.wait_for_timeout(2000)
        