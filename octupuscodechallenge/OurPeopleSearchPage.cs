using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace octupuscodechallenge
{
	public class OurPeopleSearchPage
	{

		private IWebDriver driver;
		private WebDriverWait webDriverWait;

		public OurPeopleSearchPage(IWebDriver driver)
		{
			this.driver = driver;
			webDriverWait = new WebDriverWait(driver,TimeSpan.FromSeconds(30));
		}



		private void focusOnSearchContainer()
		{
			IWebElement element = driver.FindElement(By.CssSelector(".search-container"));
			Actions searchContainerAction = new Actions(driver);
			searchContainerAction.MoveToElement(element);
			searchContainerAction.Perform();
		}

		public void acceptCookieModal()
		{
			Boolean displayed = driver.FindElement(By.CssSelector(".modal-dialog>.modal-content")).Displayed;

			IWebElement element = driver.FindElement(By.CssSelector(".modal-footer>a"));

			Actions actions = new Actions(driver);
			actions.MoveToElement(element);
			//	webDriverWait.Until(ExpectedConditions.visibilityOf(element));
			actions.Click();
			actions.Build().Perform();
		}


		public void basicSearch(String searchText)
		{

			focusOnSearchContainer();

			webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".inputOne")));
			IWebElement searchElement = driver.FindElement(By.CssSelector(".inputOne"));

			waitForElement();
			System.Diagnostics.Debug.WriteLine("waiting heere to type the value in texg box");

			Actions actions = new Actions(driver);
			actions.MoveToElement(searchElement);
			actions.Click();
			actions.SendKeys(searchText);
			actions.Build().Perform();
			System.Diagnostics.Debug.WriteLine("hope its typed ");
		}

		private void waitForElement()
		{
			Thread.Sleep(9000);
		}

		public List<String> searchResults()
		{
			webDriverWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".row.searchResults.effect-3")));

			IList<IWebElement> searchResultNames = driver.FindElements(By.CssSelector(".row.searchResults.effect-3>a>div>h2"));
			List<String> searchNameList = new List<String>();

			foreach (IWebElement searchResult in searchResultNames)
			{
				System.Diagnostics.Debug.WriteLine("searchResult.getText() = " + searchResult.Text);
				searchNameList.Add(searchResult.Text.ToLower());
			}
			return searchNameList;

		}

		public List<String> searchResultTeamNames()
		{
			IList<IWebElement> searchResultTeamNameList = driver.FindElements(By.CssSelector(".row.searchResults.effect-3>a>div>p"));
			List<String> searchNameList = new List<String>();

			foreach (IWebElement searchResult in searchResultTeamNameList)
			{
				searchNameList.Add(searchResult.Text.ToLower());
			}
			return searchNameList;

		}

		public void selectSearchResult()
		{
			IList<IWebElement> searchResultNames = driver.FindElements(By.CssSelector(".row.searchResults.effect-3"));
			IWebElement webElement = searchResultNames[0];
			webElement.Click();
			waitForElement();
		}

		public void sortBy(String order)
		{
			IWebElement selectElement = driver.FindElement(By.CssSelector(".selectOne>div>select"));
			SelectElement select = new SelectElement(selectElement);

			if (order.Equals("desc"))
			{
				select.SelectByValue("Z-A");
			}

		}


		public Boolean isPostCodeDisplayed()
		{
			//webDriverWait.until(ExpectedConditions.visibilityOf(driver.findElement(By.cssSelector(".inputTwo"))));
			return driver.FindElement(By.CssSelector(".inputTwo")).Displayed;
		}

	public void searchByPostcode(String postCode)
	{
		IWebElement element = driver.FindElement(By.CssSelector(".inputTwo"));
		Actions actions = new Actions(driver);
		actions.MoveToElement(element);
		actions.Click();
		actions.SendKeys(postCode);
		actions.Build().Perform();
	}

public void selectATeam(String teamName)
{
	focusOnSearchContainer();
	IWebElement selectElement = driver.FindElement(By.CssSelector(".selectTwo>div>select"));
SelectElement select = new SelectElement(selectElement);
			select.SelectByValue(teamName);

}

public String getCurrentUrl()
{
			return driver.Url;   }
}

}
