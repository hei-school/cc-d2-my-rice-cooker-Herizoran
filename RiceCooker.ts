import * as readlineSync from 'readline-sync';

class RiceCooker {
  private isCooking: boolean = false;
  private isWarming: boolean = false;
  private timer: NodeJS.Timeout | null = null;

  private startCooking(timeInMinutes: number): void {
    if (this.isCooking) {
      console.log("Rice cooker is already cooking. Wait for the current cycle to finish.");
      return;
    }

    console.log(`Cooking rice for ${timeInMinutes} minutes.`);
    this.isCooking = true;

    this.timer = setTimeout(() => {
      this.finishCooking();
    }, timeInMinutes * 1000 * 60);
  }

  private finishCooking(): void {
    console.log("Rice is cooked. Keep warm function activated.");
    this.isCooking = false;
    this.isWarming = true;
  }

  private stopCooking(): void {
    if (this.timer) {
      clearTimeout(this.timer);
      console.log("Cooking stopped. Rice may be undercooked.");
      this.isCooking = false;
    } else {
      console.log("No cooking in progress.");
    }
  }

  private startWarming(): void {
    if (!this.isCooking) {
      console.log("No cooking in progress. Unable to activate keep warm function.");
      return;
    }

    console.log("Keep warm function activated.");
    this.isWarming = true;
  }

  private stopWarming(): void {
    if (this.isWarming) {
      console.log("Keep warm function deactivated.");
      this.isWarming = false;
    } else {
      console.log("Keep warm function is not active.");
    }
  }

  public runCLI(): void {
    while (true) {
      console.log("\nChoose an option:");
      console.log("1. Start Cooking");
      console.log("2. Stop Cooking");
      console.log("3. Start Keep Warm");
      console.log("4. Stop Keep Warm");
      console.log("5. Exit");

      const choice = readlineSync.question("Enter your choice: ");

      switch (choice) {
        case '1':
          const cookingTime = parseInt(readlineSync.question("Enter cooking time in minutes: "));
          this.startCooking(cookingTime);
          break;
        case '2':
          this.stopCooking();
          break;
        case '3':
          this.startWarming();
          break;
        case '4':
          this.stopWarming();
          break;
        case '5':
          console.log("Turn off.");
          process.exit(0);
        default:
          console.log("Invalid choice. Please enter a number between 1 and 5.");
      }
    }
  }
}

const riceCooker = new RiceCooker();
riceCooker.runCLI();
