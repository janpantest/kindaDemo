import pytest
from playwright.sync_api import Playwright, APIRequestContext

BASE_URL = "https://demoqa.com"

@pytest.fixture(scope="module")
def api_context(playwright: Playwright) -> APIRequestContext:
    context = playwright.request.new_context(
        base_url=BASE_URL,
        extra_http_headers={
            "Content-Type": "application/json",
            "accept": "application/json",
        },
    )
    yield context
    context.dispose()
