using NUnit.Framework;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;



namespace octupuscodechallenge
{
	[TestFixture()]
	public class Test
	{
		private IWebDriver driver;
		private String URL = "https://octopusinvestments.com/adviser/about-us/our-people";

		[SetUp]
		public void SetupTest() {  
			driver = new ChromeDriver();

		}

		[TearDown]
		public void TearDownTest()
		{
			driver.Quit();
		}


	
		[Test()]
		public void ShouldSearchByName()
		{
			driver.Navigate().GoToUrl(URL);
			OurPeopleSearchPage ourPeopleSearchPage = new OurPeopleSearchPage(driver);
			PageFactory.InitElements(driver, ourPeopleSearchPage);
			ourPeopleSearchPage.acceptCookieModal();
            waitForPageLoad(driver);
			ourPeopleSearchPage.basicSearch("Adam");
			List<String> nameList = ourPeopleSearchPage.searchResults();
			Assert.IsTrue(validateNameList(nameList, "adam"));
			//ourPeopleSearchPage.selectSearchResult();
			//System.Diagnostics.Debug.WriteLine("current url ", ourPeopleSearchPage.getCurrentUrl());
            //Assert.IsTrue(ourPeopleSearchPage.getCurrentUrl().Contains("adam"));
			
		}

		[Test()]
		public void shouldSearchTextSortByDescAndVerifyTheResult()
		{
			driver.Navigate().GoToUrl(URL);
			OurPeopleSearchPage ourPeopleSearchPage = new OurPeopleSearchPage(driver);
			PageFactory.InitElements(driver, ourPeopleSearchPage);
			ourPeopleSearchPage.acceptCookieModal();
            waitForPageLoad(driver);
			ourPeopleSearchPage.basicSearch("ab");
			ourPeopleSearchPage.sortBy("desc");
			List<String> nameList = ourPeopleSearchPage.searchResults();

			Assert.IsTrue(validateNameList(nameList, "ab"));


		}

        [Test()]
        public void shouldSelectBDTAndVerifyTheResults(){
            String team = "Business development team";
			driver.Navigate().GoToUrl(URL);
			OurPeopleSearchPage ourPeopleSearchPage = new OurPeopleSearchPage(driver);
			PageFactory.InitElements(driver, ourPeopleSearchPage);
			ourPeopleSearchPage.acceptCookieModal();
			waitForPageLoad(driver);
			ourPeopleSearchPage.selectATeam(team);
            Assert.IsTrue(ourPeopleSearchPage.isPostCodeDisplayed());

			List<String> searchResultTeamName = ourPeopleSearchPage.searchResultTeamNames();
			Assert.IsTrue(validateNameList(searchResultTeamName, team));

        }

		private Boolean validateNameList(List<String> nameList, String text)
		{
			Boolean retValue = true;
			foreach (String name in nameList)
			{
			
				System.Diagnostics.Debug.WriteLine("searchResult name iin validate name list () = "+ name);
				if (!name.ToLower().Contains(text.ToLower()))
				{
					retValue = false;
				}
				System.Diagnostics.Debug.WriteLine("searchResult.getText() = " + retValue);
			}
			return retValue;
		}

		static void waitForPageLoad(IWebDriver driver)
		{
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

			    wait.Until((wdriver) => (driver as IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete"));



		    }
	}
}
