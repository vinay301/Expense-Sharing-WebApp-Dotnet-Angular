import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ChartOptions, ChartType, Chart, registerables } from 'chart.js';

import { GroupService } from '../../../modules/home/services/group.service';
import { ActivatedRoute } from '@angular/router';
import { ExpenseSplit } from '../../../core/models/expense-split.model';
import { Expense } from '../../../core/models/expense.model';
import { UserGroup } from '../../../core/models/user-group.model';
import { Group } from '../../../core/models/group.model';
Chart.register(...registerables);
@Component({
  selector: 'app-PieChart',
  templateUrl: './PieChart.component.html',
  styleUrls: ['./PieChart.component.css']
})
export class PieChartComponent implements OnChanges, OnInit {

//   constructor(private groupService: GroupService, private route: ActivatedRoute) { }

//   @Input() expenses: Expense[] = [];
//   @Input() users: any[] = [];
// public chart: any;
// ngOnChanges(changes: SimpleChanges): void {
//   if (changes['expenses']) {
//     console.log('Expenses input changed:', this.expenses); // Add this log
//     this.updateChartData(this.expenses);
//   }
// }

//   updateChartData(expenses: Expense[]) {
//     const expensesByUser: { [key: string]: number } = {};

//     if (!expenses) {
//       console.error('Expenses are not defined');
//       return;
//     }

//     expenses.forEach((expense: Expense) => {
//       if (!expensesByUser[expense.paidByUserId]) {
//         expensesByUser[expense.paidByUserId] = 0;
//       }
//       expensesByUser[expense.paidByUserId] += expense.amount;

//       expense.expenseSplits.forEach((split: { userId: string | number; isSettled: any; amountOwed: number; }) => {
//         if (!expensesByUser[split.userId]) {
//           expensesByUser[split.userId] = 0;
//         }
//         if (split.isSettled) {
//           expensesByUser[split.userId] -= split.amountOwed;
//         }
//       });
//     });


//     const userNames = Object.keys(expensesByUser).map(userId => {
//       const user = this.users.find(user => user.id === userId);
//       return user ? user.name : 'Unknown';
//     });


//     const expenseAmounts = Object.values(expensesByUser);
//     console.log(expenseAmounts)
//     console.log(userNames)
//     this.config.data.labels = userNames;
//     this.config.data.datasets[0].data = expenseAmounts;
//     this.chart.update();
 
//   }

//   public config: any = {
//     type: 'doughnut',
//     data: {
//       labels: [],
//       datasets: [{
//         label: 'Expenses',
//         data: [],
//         backgroundColor: [
//           'rgb(255, 99, 132)',
//           'rgb(54, 162, 235)',
//           'rgb(255, 205, 86)',
//           'rgb(75, 192, 192)',
//           'rgb(153, 102, 255)',
//           'rgb(255, 159, 64)'
//         ],
//         hoverOffset: 4
//       }]
//     },
//     options: {
//       aspectRatio: 1
//     }
//   };

//   ngAfterViewInit() {
//     this.chart = new Chart('MyChart', this.config);
//   }

@Input() expenses: Expense[] = [];

public chart: Chart | undefined;
public chartData: number[] = [];
public chartLabels: string[] = [];
public chartColors: string[] = [
  'rgb(255, 99, 132)',
  'rgb(54, 162, 235)',
  'rgb(255, 205, 86)'
];

constructor() {}

ngOnInit(): void {
  this.initializeChart();
}

ngOnChanges(changes: SimpleChanges): void {
  if (changes['expenses']) {
    this.updateChartData();
  }
}

initializeChart(): void {
  const config = {
    type: 'doughnut' as ChartType,
    data: {
      labels: this.chartLabels,
      datasets: [{
        data: this.chartData,
        backgroundColor: this.chartColors,
        hoverOffset: 4
      }]
    },
    options: {
      aspectRatio: 1
    } as ChartOptions
  };

  this.chart = new Chart('MyChart', config);
}

updateChartData(): void {
  const expensesByUser: { [key: string]: number } = {};
  const userNamesById: { [key: string]: string } = {};

  // this.expenses.forEach(expense => {
  //   const paidByUserId = expense.paidByUserId;
  //   const paidByUserName = expense.paidByUser?.name ?? 'Unknown';
  //   if (!expensesByUser[paidByUserId]) {
  //     expensesByUser[paidByUserId] = 0;
  //     userNamesById[paidByUserId] = paidByUserName;
  //   }
  //   expensesByUser[paidByUserId] += expense.amount;
  // });
  this.expenses.forEach(expense => {
    const paidByUserId = expense.paidByUser.id;
    const paidByUserName = expense.paidByUser?.name ?? 'Unknown';
    if (!expensesByUser[paidByUserId]) {
      expensesByUser[paidByUserId] = 0;
      userNamesById[paidByUserId] = paidByUserName;
    }
    
    expensesByUser[paidByUserId] += expense.amount;
    expense.expenseSplits.forEach((split: { isSettled: any; owedUser: { id: any; name: string; }; amountOwed: number; }) => {
      if (split.isSettled && split.owedUser) {
        const owedUserId = split.owedUser.id;
        if (!expensesByUser[owedUserId]) {
          expensesByUser[owedUserId] = 0;
          userNamesById[owedUserId] = split.owedUser.name ?? 'Unknown';
        }
        expensesByUser[owedUserId] -= split.amountOwed;
        expensesByUser[paidByUserId] -= split.amountOwed;
      }
    });
  });
  console.log(expensesByUser);
  this.chartData = Object.values(expensesByUser);
  this.chartLabels = Object.keys(expensesByUser).map(userId => userNamesById[userId]);

  if (this.chart) {
    this.chart.data.labels = this.chartLabels;
    this.chart.data.datasets[0].data = this.chartData;
    this.chart.update();
  }
}


}
