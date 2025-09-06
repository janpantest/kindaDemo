from playwright.sync_api import Page, expect, Locator

def click_if_visible(locator: Locator):
    elementVisible = if_element_visibe(locator)
    if (elementVisible):
        locator.click()

def if_element_visibe(locator: Locator):
    try:
        locator.wait_for(state='visible', timeout=5000)
        return True
    except:
        return False