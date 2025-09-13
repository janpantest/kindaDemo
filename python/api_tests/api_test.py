import os
import time
import pytest
from pathlib import Path
from dotenv import load_dotenv
from typing import Generator
from playwright.sync_api import Playwright, APIRequestContext
from helpers.api_keys import Keys

# dotenv_path = Path(__file__).parent.parent / ".env"  # points to root
# load_dotenv(dotenv_path=dotenv_path)
load_dotenv()

BASE_URL = "https://demoqa.com"
USERNAME_PREFIX = os.getenv("USERNAME_PREFIX")
PASSWORD = os.getenv("PASSWORD")

print("USERNAME_PREFIX:", USERNAME_PREFIX)
print("PASSWORD:", PASSWORD)


# ----------------------
# Payload helpers
# ----------------------
def payload_create_user(user_name: str, password: str) -> dict:
    return {"userName": user_name, "password": password}


def payload_generate_token(user_name: str, password: str) -> dict:
    return {"userName": user_name, "password": password}


def payload_authorize_user(user_name: str, password: str) -> dict:
    return {"userName": user_name, "password": password}


def payload_add_book(user_id: str) -> dict:
    return {
        "userId": user_id,
        "collectionOfIsbns": [{"isbn": "9781449325862"}],
    }


# ----------------------
# Fixture: API context
# ----------------------
@pytest.fixture(scope="module")
def api_context(playwright: Playwright) -> Generator[APIRequestContext, None, None]:
    context = playwright.request.new_context(
        base_url=BASE_URL,
        extra_http_headers={
            "Content-Type": "application/json",
            "accept": "application/json",
        },
    )
    yield context
    context.dispose()


# ----------------------
# Test: chained API calls
# ----------------------
def test_chained_api_calls(api_context: APIRequestContext):
    user_name = f"{USERNAME_PREFIX}{int(time.time() * 1000)}"
    PASSWORD = os.getenv("PASSWORD")

    # 1. Create user
    create_user_response = api_context.post(
        "/Account/v1/User",
        data=payload_create_user(user_name, PASSWORD),
    )
    print(create_user_response.status)
    print(create_user_response.text())
    assert create_user_response.status == 201
    user_id = create_user_response.json()[Keys.USER_ID]
    assert user_id

    # 2. Generate token
    token_response = api_context.post(
        "/Account/v1/GenerateToken",
        data=payload_generate_token(user_name, PASSWORD),
    )
    print(token_response.text())
    assert token_response.status == 200
    token = token_response.json()[Keys.TOKEN]
    assert token

    # 3. Authorize user
    auth_response = api_context.post(
        "/Account/v1/Authorized",
        data=payload_authorize_user(user_name, PASSWORD),
    )
    assert auth_response.status == 200
    assert auth_response.json() is True

    # 4. Add book
    add_book_response = api_context.post(
        "/BookStore/v1/Books",
        headers={"Authorization": f"Bearer {token}"},
        data=payload_add_book(user_id),
    )
    assert add_book_response.status == 201
    add_book_body = add_book_response.json()
    print(add_book_body)
    assert Keys.BOOKS in add_book_body
