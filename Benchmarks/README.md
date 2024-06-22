Model: llama2, Time: 313.178760895s
Model: llama3, Time: 347.545391716s
Model: stablelm-zephyr, Time: 262.244742756s
Model: llama2+ctx, Time: 2160.258955724s
Model: llama3+ctx, Time: 237.954082414s
Model: stablelm-zephyr+ctx, Time: 184.623767314s
                     total_duration  load_duration  prompt_eval_count   
llama2                   313.178761       3.013889                106  \
llama3                   347.545392      35.125766                 87   
stablelm-zephyr          262.244743       6.336809                 96   
llama2+ctx              2160.258956      30.345983                139   
llama3+ctx               237.954082      31.340429                120   
stablelm-zephyr+ctx      184.623767       7.163820                129   

                     eval_count  prompt_eval_duration  eval_duration   
llama2                     1306              0.880517     309.282949  \
llama3                      652              1.619323     310.792324   
stablelm-zephyr             960              2.895785     253.009869   
llama2+ctx                 1668             21.318885    2108.577172   
llama3+ctx                  517              8.596607     198.009424   
stablelm-zephyr+ctx         749              3.246992     174.211387   

                     calc_duration  prompt_rate  eval_rate  
llama2                  310.164871   120.383820   4.222671  
llama3                  312.419625    53.726156   2.097864  
stablelm-zephyr         255.907934    33.151632   3.794318  
llama2+ctx             2129.912973     6.520041   0.791055  
llama3+ctx              206.613653    13.958996   2.610987  
stablelm-zephyr+ctx     177.459947    39.729078   4.299375 